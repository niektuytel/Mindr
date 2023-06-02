terraform {
  required_version = ">=0.12"
  # backend "azurerm" {}// using backend (comment OUT on local device test )

  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = ">= 3.19.1"
    }
    azuread = {
      source  = "hashicorp/azuread"
      version = ">= 2.7.0"
    }
    random = {
      source  = "hashicorp/random"
      version = ">= 3.1"
    }
  }
}

provider "azurerm" {
  features {}
}