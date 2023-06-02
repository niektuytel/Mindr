
resource "azurerm_service_plan" "this" {
  name                = var.app_plan_name
  resource_group_name = var.resource_group_name
  location            = var.location
  os_type             = "Linux"
  sku_name            = "B1"
}

resource "azurerm_linux_web_app" "oidc" {
  name                = var.app_oidc_name
  resource_group_name = var.resource_group_name
  location            = var.location
  service_plan_id     = azurerm_service_plan.this.id

  site_config {
  }
  
  app_settings = {
  }
  
  connection_string {
    name  = "DefaultConnection"
    type  = "MySql"
    value = "Server=tcp:${var.database_server_name}.database.windows.net,1433;Initial Catalog=${var.database_oidc_name};Persist Security Info=False;User ID=${var.dbserver_admin_name};Password=${var.dbserver_admin_password};MultipleActiveResultSets=False;Encrypt=True;Connection Timeout=30;"
  }
}

resource "azurerm_linux_web_app" "api" {
  name                = var.app_api_name
  resource_group_name = var.resource_group_name
  location            = var.location
  service_plan_id     = azurerm_service_plan.this.id

  site_config {
  }
  
  app_settings = {
    IdentityServer__ClientId = "Mindr.Api"
    IdentityServer__Authority = "https://localhost:44319"
    IdentityServer__Audience = "mindr_api"
    Google__ClientId = "889842565350-hmf83o017dfqpg6akp35c941ocj5arha.apps.googleusercontent.com"
    Google__ClientSecret = "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"
  }
  
  connection_string {
    name  = "SqlDatabase"
    type  = "MySql"
    value = "Server=tcp:${var.database_server_name}.database.windows.net,1433;Initial Catalog=${var.database_api_name};Persist Security Info=False;User ID=${var.dbserver_admin_name};Password=${var.dbserver_admin_password};MultipleActiveResultSets=False;Encrypt=True;Connection Timeout=30;"
  }
}

# TODO
# resource "azurerm_linux_web_app" "website" {
#   name                = var.app_website_name
#   resource_group_name = var.resource_group_name
#   location            = var.location
#   service_plan_id     = azurerm_service_plan.this.id

#   site_config {
#   }
  
#   app_settings = {
#   }
  
#   connection_string {
#     name  = "DefaultConnection"
#     type  = "MySql"
#     value = "Server=tcp:${var.database_server_name}.database.windows.net,1433;Initial Catalog=${var.database_api_name};Persist Security Info=False;User ID=${var.dbserver_admin_name};Password=${var.dbserver_admin_password};MultipleActiveResultSets=False;Encrypt=True;Connection Timeout=30;"
#   }
# }

