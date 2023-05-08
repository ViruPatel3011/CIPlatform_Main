using Azure.Core;
using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Repository
{
    public class ForgotRepository:IForgotRepository
    {
        private readonly CiPlatformContext _ciPlatformContext;

        public ForgotRepository(CiPlatformContext ciPlatformContext)
        {
            _ciPlatformContext = ciPlatformContext;
     
     
        }

        public bool CheckUser(ForgotVM forgotVM)
        {
            var isValidEmail=_ciPlatformContext.Users.FirstOrDefault(x=>x.Email == forgotVM.Email);
            if(isValidEmail == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool SendMail(ForgotVM forgotVM, string url)
        {

            // Generate a password reset token for the user
            var token = Guid.NewGuid().ToString();



            // Store the token in the password resets table with the user's email
            var passwordReset = new PasswordReset
            {
                Email = forgotVM.Email,
                Token = token
            };


            _ciPlatformContext.PasswordResets.Add(passwordReset);
            _ciPlatformContext.SaveChanges();

            // Send an email with the password reset link to the user's email address
            /* var resetLink = url.Action("ResetPassword", "ResetPassword", new { email = forgotVM.Email, token }, Request.Scheme);*/
            // Send email to user with reset password link

            var resetLink = url.Replace("{token}", token);

            var fromAddress = new MailAddress("himansugami21@gmail.com", "Viral Patel");
            var toAddress = new MailAddress(forgotVM.Email);
            var subject = "Password reset request";
            var body = $"Hi,<br /><br />Please click on the following link to reset your password:<br /><br /><a href='{resetLink}'>{resetLink}</a>";


            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("himansugami21@gmail.com", "usyghvhscylmpsva"),
                EnableSsl = true
            };
            smtpClient.Send(message);
            return true;
        }




    }
}
