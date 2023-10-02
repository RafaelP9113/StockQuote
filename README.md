# Stock Quote Alert

Este é um aplicativo de console que monitora a cotação de um ativo da B3 e envia alertas via e-mail se a cotação atingir determinados níveis.

## Funcionalidades

- Monitoramento contínuo da cotação do ativo
- Envio de e-mails de alerta para compra e venda com base nos preços de referência
- Gráfico interativo para visualização da tendência de preços

## Como Usar

1. **Configuração do Projeto**

   Certifique-se de ter o .NET Core SDK instalado em sua máquina.

2. **Envio de Email**
   
   Atualize as credenciais de envio de email

3. **Compilação e Execução**

   Abra um terminal na pasta do projeto e execute o seguinte comando:

   ```bash
   dotnet run

2. **Publicação como Executável**

   Abra um terminal na pasta do projeto e execute o seguinte comando:

   ```bash
   dotnet publish -c Release -r win-x64 --self-contained

