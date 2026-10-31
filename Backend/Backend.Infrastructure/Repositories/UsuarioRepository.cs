using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace Backend.Infrastructure.Repositories
{
    public class UsuarioRepository(
        AppDbContext context,
        IPasswordHasher contraseniaHasher) : IUsuarioRepository
    {

        public async Task<Usuario?> GetByEmailAsync(string correoElectronico, CancellationToken ct = default)
         => await context.Usuario.AsNoTracking()
            .Include(x => x.Persona)
            .FirstOrDefaultAsync(u => u.CorreoElectronico == correoElectronico, ct);

        public async Task<bool> EmailExistsAsync(string email, CancellationToken ct = default)
        => await context.Usuario.AsNoTracking().AnyAsync(u => u.CorreoElectronico == email, ct);

        public async Task UpdateUsuariopAsync(int psicopedagogoId, string token, DateTime ExpiresAt, CancellationToken ct = default)
        {
            var existingPsicopedagogo = await context.Usuario.FirstOrDefaultAsync(p => p.PersonaId == psicopedagogoId, ct);

            if (existingPsicopedagogo == null) return;

            existingPsicopedagogo.TokenRestablecimiento = token;
            existingPsicopedagogo.FechaExpiracionTokenRestablecimiento = ExpiresAt;

            await context.SaveChangesAsync(ct);
        }

        public async Task ValidatePasswordResetTokenAsync(string token, string nuevaContrasenia, CancellationToken ct = default)
        {
            var existingUsuario = await
                   context.Usuario
                   .FirstOrDefaultAsync(u => u.TokenRestablecimiento == token && u.FechaExpiracionTokenRestablecimiento > DateTime.Now);

            if (existingUsuario == null) return;

            existingUsuario.Contrasenia = contraseniaHasher.Hash(nuevaContrasenia);
            existingUsuario.TokenRestablecimiento = null;
            existingUsuario.FechaExpiracionTokenRestablecimiento = DateTime.MinValue;
            existingUsuario.FechaRecuperacion = DateTime.Now;

            await context.SaveChangesAsync(ct);
        }

        public async Task sendEmail(string correoElectronico, string token, CancellationToken ct = default)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("", ""));
            emailMessage.To.Add(new MailboxAddress("Destinatario", correoElectronico));
            emailMessage.Subject = "Solicitud de recuperación de contraseña";
            emailMessage.Body = new TextPart("html")
            {
                Text = $"""
                        <!DOCTYPE html>
                        <html lang="es">
                        <head>
                          <meta charset="UTF-8">
                          <meta name="viewport" content="width=device-width, initial-scale=1.0">
                          <title>Restablecé tu contraseña</title>
                        </head>

                        <body style="margin:0; padding:0; background-color:#ffffff;
                                     font-family:Arial,Helvetica,sans-serif; color:#24292f;">

                          <table role="presentation" width="100%" cellpadding="0"
                                 cellspacing="0" border="0" style="background:#ffffff;">
                            <tr>
                              <td align="center" style="padding:24px 12px;">

                                <table role="presentation" width="100%" cellpadding="0"
                                       cellspacing="0" border="0"
                                       style="max-width:600px;">

                                  <!-- Logo -->
                                  <tr>
                                    <td align="center" style="padding:0 0 20px;">
                                      <table role="presentation" cellpadding="0" cellspacing="0"
                                             border="0">
                                        <tr>
                                          <td align="center" valign="middle"
                                              width="48" height="48"
                                              style="width:48px;height:48px;
                                                     background-color:#2563eb;
                                                     border-radius:50%;">
                                            <img
                                              src="https://hackmd-prod-images.s3.ap-northeast-1.amazonaws.com/uploads/9e3aa6f6-484b-49cf-b687-88a3f3945ba9.png?AWSAccessKeyId=AKIA3XSAAW6AWSKNINWO&Expires=1792881680&Signature=H%2BiM3kX7shd%2BnupD6v8Osadhn8s%3D"
                                              width="34"
                                              height="34"
                                              alt="Logo"
                                              style="display:block;width:34px;height:34px;border:0;"
                                            >
                                          </td>
                                        </tr>
                                      </table>
                                    </td>
                                  </tr>

                                  <!-- Título -->
                                  <tr>
                                    <td align="center"
                                        style="padding:0 0 20px;font-size:23px;
                                               line-height:30px;font-weight:400;color:#24292f;">
                                      Restablecé tu contraseña
                                    </td>
                                  </tr>

                                  <!-- Contenido -->
                                  <tr>
                                    <td style="border:1px solid #d8dee4;border-radius:6px;
                                               padding:24px 20px;background-color:#ffffff;">

                                      <h2 style="margin:0 0 22px;text-align:center;
                                                 font-size:20px;line-height:26px;color:#24292f;">
                                        Configuración de la contraseña
                                      </h2>

                                      <p style="margin:12px 0;font-size:14px;
                                                line-height:22px;color:#24292f;">
                                        Nos enteramos de que perdiste tu contraseña.
                                        ¡Lo sentimos!
                                      </p>

                                      <p style="margin:12px 0;font-size:14px;
                                                line-height:22px;color:#24292f;">
                                        ¡Pero no te preocupes! Podés usar el siguiente botón
                                        para restablecer tu contraseña:
                                      </p>

                                      <!-- Botón -->
                                      <table role="presentation" align="center" cellpadding="0"
                                             cellspacing="0" border="0"
                                             style="margin:22px auto;">
                                        <tr>
                                          <td align="center" bgcolor="#2563eb"
                                              style="background-color:#2563eb;border-radius:7px;">
                                            <a href="http://localhost:5173/restaurar-contraseña/?token={Uri.EscapeDataString(token)}"
                                               target="_blank"
                                               style="display:inline-block;padding:15px 24px;
                                                      background-color:#2563eb;border:1px solid #2563eb;
                                                      border-radius:7px;color:#ffffff !important;
                                                      font-family:Arial,Helvetica,sans-serif;
                                                      font-size:14px;font-weight:bold;
                                                      line-height:20px;text-align:center;
                                                      text-decoration:none;">
                                              <span style="color:#ffffff;">
                                                Restablecer mi contraseña
                                              </span>
                                            </a>
                                          </td>
                                        </tr>
                                      </table>

                                      <p style="margin:16px 0;font-size:14px;
                                                line-height:22px;color:#24292f;">
                                        Si no utilizás este enlace dentro de las próximas
                                        24 horas, vencerá.
                                        <a href="http://localhost:5173/recuperar"
                                           style="color:#0969da;text-decoration:underline;">
                                          Hacé clic acá para obtener un nuevo enlace
                                          de restablecimiento de contraseña.
                                        </a>
                                      </p>

                                      <p style="margin:16px 0 0;font-size:14px;
                                                line-height:22px;color:#24292f;">
                                        Gracias,<br>
                                        El equipo de soporte
                                      </p>

                                    </td>
                                  </tr>

                                  <!-- Pie -->
                                  <tr>
                                    <td align="center"
                                        style="padding:28px 20px 0;font-size:12px;
                                               line-height:20px;color:#656d76;">
                                      Recibís este correo electrónico porque se solicitó
                                      restablecer la contraseña de tu cuenta.
                                    </td>
                                  </tr>

                                </table>
                              </td>
                            </tr>
                          </table>

                        </body>
                        </html>
                        """
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true, ct);
                await client.AuthenticateAsync("", "", ct);
                await client.SendAsync(emailMessage, ct);
                await client.DisconnectAsync(true, ct);
            }
        }
    }
}
