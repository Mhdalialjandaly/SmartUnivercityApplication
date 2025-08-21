using Microsoft.AspNetCore.Identity;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagement.Blazor.Services
{
    public class EmailSender : IEmailSender<User>
    {
        public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
        {
            // تنفيذ إرسال رابط التأكيد
            await SendEmailAsync(email, "تأكيد الحساب", $"يرجى تأكيد حسابك من خلال الرابط التالي: {confirmationLink}");
        }

        public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
        {
            // تنفيذ إرسال رابط إعادة تعيين كلمة المرور
            await SendEmailAsync(email, "إعادة تعيين كلمة المرور", $"استخدم الرابط التالي لإعادة تعيين كلمة المرور: {resetLink}");
        }

        public async Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
        {
            // تنفيذ إرسال كود إعادة تعيين كلمة المرور
            await SendEmailAsync(email, "كود إعادة تعيين كلمة المرور", $"كود إعادة التعيين الخاص بك هو: {resetCode}");
        }

        private async Task SendEmailAsync(string email, string subject, string message)
        {
            // تنفيذ عملية إرسال البريد الإلكتروني الفعلية
            // يمكنك استخدام SendGrid أو SMTP أو أي خدمة بريد إلكتروني أخرى

            // مثال بسيط (لا تستخدم في الإنتاج):
            Console.WriteLine($"إرسال بريد إلى: {email}");
            Console.WriteLine($"الموضوع: {subject}");
            Console.WriteLine($"الرسالة: {message}");

            await Task.CompletedTask;
        }
    }
}
