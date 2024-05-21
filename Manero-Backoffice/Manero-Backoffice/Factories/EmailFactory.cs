using Manero_Backoffice.Business.Models;

namespace Manero_Backoffice.Factories;

public class EmailFactory
{
    public static EmailRequest CreateEmailRequest(string email, string password, string firstname, string role)
    {
        return new EmailRequest
        {
            To = email,
            Subject = $"Welcome to Manero Admin!",
            HtmlBody = $@"
                        <html lang='en'>
                            <head>
                                <meta charset='UTF-8'>
                                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                                <title>Verification Code</title>
                            </head>
                            <body style='box-sizing: border-box; padding: 0; margin: 0; width: 100%; display: flex; justify-content: center;'>
                                <div style='background-color: #F4F4F4; color: #191919; max-width: 550px; height: fit-content; margin-top: 1rem;'>
                                    <div style='background-color: #62c5ae; color: white; text-align: center; padding: 2rem 0 2rem; '>
                                        <h1 style='color: #ffffffdc; font-weight: 400; font-family: Verdana, Geneva, Tahoma, sans-serif; '>Login Details</h1>
                                    </div>
                                    <div style='background-color:#f4f4f4; color: #1c2e3b; border-radius: .5rem; padding: 1rem 2rem; margin: -4rem 1.5rem 1.5rem; box-shadow: 0px 0px 30px -5px #191919; '>
                                        <p style='font-weight: 700; font-size: large;'>Hey there {firstname}!</p>
                                        <p>You are now registered as {role} at Manero. Here are your login details. Use these to login to your <a href='https://manerobackoffice.azurewebsites.net/'>Manero Admin app</a>.</p>
                                        <div style='color: #418373; font-weight: 700; text-align:left; font-size: 14px;'>
                                        Email: {email}
                                        </div>
                                        <div style='color: #418373; font-weight: 700; text-align:left; font-size: 14px;'>
                                        Password: {password}
                                        </div>
                                        <div style='color: #1c2e3b; font-size: 11px;'>
                                            <p style='margin: 1.5rem 0 0; font-weight: 700; font-size: larger;'>Take care,</p>
                                            <p style='margin: .25rem 0; text-transform: uppercase; '>Manero group</p>
                                        </div>
                                    </div>
                                </div>
                            </body>
                        </html>
                    "
        };
    }
}
