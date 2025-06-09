# Sistema de gerencimento de Pacientes e Exames para laboratório com envio de SMS usando Twilio

• Aplicativo Web Crud feito com C# ASP.NET CORE

• Banco de dados SQL SERVER

• .NET 8

• Entity Framework

• Razor Pages

• Bootstrap

• Twilio API

Banco de dados SQL SERVER

# Importante: Mude o Server do DefaultConnection no appsettings.json, para o nome do servidor de sua máquina

Programas Utilizados

• Visual Studio

• Sql Server 20

# Imporante: Para usar o Twilio você deve se cadastrar no site da Api: https://www.twilio.com/pt-br, e alterar no appsettings.json para seus respectivos dados:

  "Twilio": {
    "AccountSid": "SEU_ACCOUNT_SID_AQUI",
    "AuthToken": "SEU_AUTH_TOKEN_AQUI",
    "FromNumber": "+Seu_Numero_Twillio_aqui"
  }
  
  #Importante: Para enviar por SMS você deve usar o número Twillio e não o proprio numero do seu celular, e a versão FREE do Twillio só envia SMS para números cadastrados no twillio na conta de quem vai enviar o SMS, para enviar para qualquer numero deve se utilizar a versão paga


