resource "azurerm_mssql_server" "this" {
  name                          = var.database_server_name
  resource_group_name           = var.resource_group_name
  location                      = var.location
  version                       = "12.0"
  administrator_login           = var.dbserver_admin_name
  administrator_login_password  = var.dbserver_admin_password
}

// Allow azure resources enabled for connection
resource "azurerm_mssql_firewall_rule" "this" {
  name                = "${azurerm_mssql_server.this.name}rule"
  server_id           = azurerm_mssql_server.this.id
  start_ip_address    = "0.0.0.0"
  end_ip_address      = "0.0.0.0"
}

resource "azurerm_mssql_database" "db1" {
  name           = var.database1_name
  server_id      = azurerm_mssql_server.this.id
  sku_name       = "Basic"
}

resource "azurerm_mssql_database" "db2" {
  name           = var.database2_name
  server_id      = azurerm_mssql_server.this.id
  sku_name       = "Basic"
}


