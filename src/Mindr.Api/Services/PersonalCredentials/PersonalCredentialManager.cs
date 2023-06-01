using Hangfire;
using Mindr.Api.Exceptions;
using Mindr.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Graph.ExternalConnectors;
using Mindr.Api.Services.ConnectorEvents;
using System.Net;
using AutoMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Mindr.Domain.HttpRunner.Models;
using Mindr.Api.Models.Connectors;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.Domain.Models.DTO.Personal;
using Mindr.Domain.Enums;

namespace Mindr.Api.Services.PersonalCredentials
{
    public class PersonalCredentialManager : IPersonalCredentialManager
    {
        private readonly IPersonalCredentialValidator _connectorValidator;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;

        public PersonalCredentialManager(IPersonalCredentialValidator connectorValidator, IMapper mapper, ApplicationContext context)
        {
            _connectorValidator = connectorValidator;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<PersonalCredential>> GetAllById(string userId, Guid id)
        {
            _connectorValidator.ThrowOnInvalidUserId(userId);

            var entities = await _context.PersonalCredentials
                .Where(item => item.UserId == userId)
                .Where(item => item.Id == id)
                .ToListAsync();

            return entities;
        }

        public async Task<IEnumerable<PersonalCredential>> GetAll(string userId)
        {
            _connectorValidator.ThrowOnInvalidUserId(userId);

            var entities = await _context.PersonalCredentials
                .Where(item => item.UserId == userId)
                .ToListAsync();

            return entities;
        }

        public async Task<PersonalCredential> Upsert(string userId, PersonalCredential input)
        {
            input.UserId = userId;
            _connectorValidator.ThrowOnInvalidUserId(userId);

            var entity = await _context.PersonalCredentials
                .Where(item => item.UserId == userId)
                .FirstOrDefaultAsync(x => x.Id == input.Id);

            if (entity == null)
            {
                entity = input;
                _context.PersonalCredentials.Add(entity);
            }
            else
            {
                entity!.AccessToken = input.AccessToken;
                entity!.ExpiresIn = input.ExpiresIn;
                entity!.RefreshToken = input.RefreshToken;
                entity!.Scope = input.Scope;
                entity!.TokenType = input.TokenType;

                _context.PersonalCredentials.Update(entity);
            }

            await _context.SaveChangesAsync();
            return entity!;
        }

        public async Task<PersonalCredential> Update(string userId, Guid id, PersonalCredentialDTO input)
        {
            _connectorValidator.ThrowOnInvalidUserId(userId);
            var entity = await _context.PersonalCredentials
                .Where(item => item.UserId == userId)
                .FirstOrDefaultAsync(x => x.Id == id);

            _connectorValidator.ThrowOnNullPersonalCredential(id, entity);

            entity!.AccessToken = input.AccessToken;
            entity!.ExpiresIn = input.ExpiresIn;
            entity!.RefreshToken = input.RefreshToken;
            entity!.Scope = input.Scope;
            entity!.TokenType = input.TokenType;

            _context.PersonalCredentials.Update(entity);
            await _context.SaveChangesAsync();

            return entity!;
        }

        public async Task<PersonalCredential> Delete(string userId, Guid id)
        {
            _connectorValidator.ThrowOnInvalidUserId(userId);
            var entity = await _context.PersonalCredentials
                .Where(item => item.UserId == userId)
                .FirstOrDefaultAsync(x => x.Id == id);

            _connectorValidator.ThrowOnNullPersonalCredential(id, entity);

            // delete entity
            _context.PersonalCredentials.Remove(entity!);
            await _context.SaveChangesAsync();

            return entity!;
        }

    }
}