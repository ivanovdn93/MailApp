# MailApp
### Project Description
MailApp is an ASP.NET Core 3.1 Web Api application that sends an email and logs it into the database with the result of sending and error message in case of unsuccessful sending.
Json data model:
{
  "subject": "string",
  "body": "string",
  "recipients": [ "string" ]
}