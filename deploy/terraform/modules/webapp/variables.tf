variable "resource_group_name" {
  description = "The name of the resource group"
  type        = string
}

variable "location" {
  description = "The location for the resource"
  type        = string
}

variable "app_plan_name" {
  description = "The name of the app plan"
  type        = string
}

variable "app_oidc_name" {
  description = "The name of the app"
  type        = string
}

variable "app_api_name" {
  description = "The name of the app"
  type        = string
}

variable "database_server_name" {
  description = "The name of the database server"
  type        = string
}

variable "database_oidc_name" {
  description = "The name of the database"
  type        = string
}

variable "database_api_name" {
  description = "The name of the database"
  type        = string
}

variable "dbserver_admin_name" {
  description = "The name of the database to access"
  type        = string
}

variable "dbserver_admin_password" {
  description = "The password of the database to access"
  type        = string
}


