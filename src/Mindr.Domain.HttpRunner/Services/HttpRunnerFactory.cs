﻿using Force.DeepCloner;
using Mindr.Domain.HttpRunner.Extensions;
using Mindr.Domain.HttpRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Mindr.Domain.HttpRunner.Services
{
    public class HttpRunnerFactory : IHttpRunnerFactory
    {
        private HttpMethod GetMethod(string method)
        {
            var key = method.Trim().ToLower();

            return key switch
            {
                "get" => HttpMethod.Get,
                "post" => HttpMethod.Post,
                "put" => HttpMethod.Put,
                "patch" => HttpMethod.Patch,
                "delete" => HttpMethod.Delete,
                "options" => HttpMethod.Options,
                "head" => HttpMethod.Head,
                _ => throw new NotImplementedException($"Method {method} is not supported"),
            };
        }

        public HttpRequestMessage CreateHttpMessage(HttpRequest input)
        {
            var request = input.DeepClone();
            request.SetVariables();

            var httpMessage = new HttpRequestMessage(GetMethod(request.Method), request.Url.Raw);
            foreach (var item in request.Header)
            {
                if (item.Key == "Content-Key")
                {
                    httpMessage.Content = new StringContent(request.Body.Raw, Encoding.UTF8, item.Value);
                }
                else
                {
                    httpMessage.Headers.Add(item.Key, item.Value);
                }
            }

            return httpMessage;
        }

        public HttpItem PrepareHttpItem(HttpItem item, IEnumerable<HttpItem> httpPipeline, HttpCollection postmanCollection)
        {
            // TODO: set global key on global values that are set in requests (postman can set values in requests that have required header field for example)

            // set item variables
            if (item.Request.Variables == null)
            {
                item.Request.Variables = item.Request.GetVariables().ToList();
            }

            foreach (var variable in item.Request.Variables)
            {
                // set other matching variables to this call
                foreach (var pipeItem in httpPipeline)
                {
                    var res = pipeItem.Request.Variables.FirstOrDefault(i => i.Key == variable.Key && !string.IsNullOrEmpty(i.Value));
                    if (res != null)
                    {
                        variable.Value = res.Value;
                        break;
                    }
                }

                // set global variable to this call
                if (string.IsNullOrEmpty(variable.Value))
                {
                    var res = postmanCollection.Variable.FirstOrDefault(i => i.Key == variable.Key && !string.IsNullOrEmpty(i.Value));
                    if (res != null)
                    {
                        variable.Value = res.Value;
                    }
                }
            }

            return item;
        }

    }
}
