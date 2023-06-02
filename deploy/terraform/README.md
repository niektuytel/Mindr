For local deployment, remove 'backend "azurerm" { }' from provider.tf
we us az cli logged in subscription for backend.

## 1. login to azure
[more info](https://learn.microsoft.com/en-us/cli/azure/manage-azure-subscriptions-azure-cli)

az login --user <myAlias@myCompany.com> -password <myPassword> --tenant <myTenantID>

## 2. deploy terraform script
[more info](https://developer.hashicorp.com/terraform/tutorials/aws-get-started/install-cli)

terraform init

terraform apply

## Possible issues
- <b>ISSUE</b>: Cannot open server 'mindrdbserver-test' requested by the login. Client with IP address '92.68.214.98' is not allowed to access the server. To enable access, use the Azure Portal or run sp_set_firewall_rule on the master database to create a firewall rule for this IP address or address range. It may take up to five minutes for this change to take effect.
Allowlist IP 92.68.214.98 on server mindrdbserver-test
<b>SOLUTION</b>: add ip to whitelist in azure, read error message
