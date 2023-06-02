variable "resource_group_name" {
  description = "The name of the resource group"
  type        = string
}

variable "location" {
  description = "The location for the resource"
  type        = string
}

variable "database_server_name" {
  description = "The name of the database server"
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

variable "database1_name" {
  description = "The name of the database"
  type        = string
}

variable "database2_name" {
  description = "The name of the database"
  type        = string
}
