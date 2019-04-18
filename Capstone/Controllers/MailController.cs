using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class MailController : Controller
{
    private SmtpClient smtpClient;
    public MailController(SmtpClient smtpClient)
    {
        this.smtpClient = smtpClient;
    }


    [HttpPost]
    public async Task<IActionResult> Post()
    {
        await this.smtpClient.SendMailAsync(new MailMessage(
            "sendersonsender@gmail.com",
            "katheryn.humphries@gmail.com",
            "Test message subject",
            "Test message body"
        ));

        return Ok("OK");

    }

    protected override void Dispose(bool disposing)
    {
        this.smtpClient.Dispose();
        base.Dispose(disposing);
    }

}  