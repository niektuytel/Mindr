module "resourcegroup" {
  source              = "../modules/resourcegroup"
  resource_group_name = "${lower(var.project_name)}-${lower(var.project_prefix)}-rg"
  location            = var.location
}

module "database" {
  source                = "../modules/database"
  resource_group_name   = module.resourcegroup.name
  location              = var.location
  database_server_name  = "${lower(var.project_name)}-${lower(var.project_prefix)}-dbserver"
  dbserver_admin_name   = var.dbserver_admin_name
  dbserver_admin_password = var.dbserver_admin_password
  database1_name         = "${lower(var.project_name)}-${lower(var.project_prefix)}-db"
  database2_name         = "${lower(var.project_name)}-${lower(var.project_prefix)}-dboidc"
}

module "webapp" {
  source                = "../modules/webapp"
  resource_group_name   = module.resourcegroup.name
  location              = var.location
  app_plan_name         = "${lower(var.project_name)}-${lower(var.project_prefix)}-appplan"
  app_oidc_name         = "${lower(var.project_name)}-${lower(var.project_prefix)}-appoidc"
  app_api_name          = "${lower(var.project_name)}-${lower(var.project_prefix)}-appapi"
  database_server_name  = module.database.database_server_name
  database_oidc_name    = module.database.database2_name
  database_api_name    = module.database.database1_name
  dbserver_admin_name   = var.dbserver_admin_name
  dbserver_admin_password = var.dbserver_admin_password
}
