# 📈 QuotaAnalyzer  


Projeto desenvolvido para **analisar cotações de ativos** e **enviar alertas por e-mail** quando condições favoráveis de **compra** ou **venda** forem identificadas. Foi utilizada a api do Brapi.dev
---

## 🚀 Antes de Usar  

Antes de executar o projeto, crie um arquivo **`.env`** na raiz do diretório com o seguinte conteúdo:  

```env
EMAIL_USER=seu_email@gmail.com
EMAIL_PASS=sua_senha_ou_chave_de_app
EMAIL_RECEIVER=email_destinatario@gmail.com
SMTP_HOST=smtp.gmail.com
SMTP_PORT=587
API_URL=https://brapi.dev/api/quote/
```
---
## ⚠️ Atenção

Caso sua conta de e-mail utilize autenticação em dois fatores (2FA), será necessário **gerar uma senha** de aplicativo para o envio de mensagens.

