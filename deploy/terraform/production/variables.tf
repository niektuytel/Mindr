variable "project_name" {
    description = "The name of the resources"
    default = "Mindr"
    type = string    
}

variable "project_prefix" {
    description = "The name of the resources"
    default = "Test"
    type = string    
}

variable "location" {
    description = "The location for the resources"
    default = "westeurope"
    type = string    
}

variable "dbserver_admin_name" {
    description = "The name of the owner"
    default = "owner"
    type = string    
}

variable "dbserver_admin_password" {
    description = "The name of the owner password"
    default = "Password1234#"
    type = string    
}

# variable "app_id" {
#     description = "Application id that is been used for this api"
#     default = "23dd28e3-cf2a-4741-8b93-d31c261262f8"
#     type = string    
# }


