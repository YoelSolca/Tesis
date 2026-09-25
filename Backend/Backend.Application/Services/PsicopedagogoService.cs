using Backend.Application.Common;
using Backend.Application.DTO;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Backend.Application.Services
{
    public class PsicopedagogoService(
        IPsicopedagogoRepository repository,
        IPasswordHasher contraseniaHasher,
        //IUsuarioActual usuarioActual,
        ILogger<PsicopedagogoService> logger): IPsicopedagogoService
    {
        public async Task<Result<PsicopedagogoDto>> CreatepsicopedagogoAsync(CreatePsicopedagogoRequest request, CancellationToken ct = default)
        {
            var psicopedagogo = new Psicopedagogo
            {
                Avatar = request.Avatar,
                Persona = new Persona
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    Telefono = request.Telefono,
                    Documento = request.Documento,
                    Genero = request.Genero,
                    FechaNacimiento = request.FechaNacimiento
                },
                Usuario = new Usuario
                {
                    CorreoElectronico = request.CorreoElectronico,
                    Contrasenia = contraseniaHasher.Hash(request.Contrasenia),
                    FechaAlta = DateTime.Now,
                }
            };

            await repository.AddPsicopedagogoAsync(psicopedagogo, ct);
            logger.LogInformation("Creado psicopedagogo {PsicopedagogoId}", psicopedagogo.PersonaId);
            return Result<PsicopedagogoDto>.Success(PsicopedagogoDto.FromEntity(psicopedagogo));
        }

        public async Task<Result<PsicopedagogoDto>> GetByPsicopedagogoIdAsync(int psicopedagogoId, CancellationToken ct = default)
        {
            var psicopedagogo = await repository.GetByPsicopedagogoIdAsync(psicopedagogoId, ct);
             
           
            return psicopedagogo is null
                ? Result<PsicopedagogoDto>.Failure($"Psicopedagogo con el id {psicopedagogoId} no fue encontrado.")
                : Result<PsicopedagogoDto>.Success(PsicopedagogoDto.FromEntity(psicopedagogo));
        }
        public async Task<Result<PsicopedagogoDto>> UpdateAsync(int psicopedagogoId, UpsertPsicopedagogoRequest request, CancellationToken ct = default)
        {
            var existing = await repository.GetByPsicopedagogoIdAsync(psicopedagogoId, ct);

            if(existing is null)
            {
                return Result<PsicopedagogoDto>.Failure($"Psicopedagogo con el id {psicopedagogoId} no fue encontrado.");
            }

            var psicopedagogo = new Psicopedagogo
            {
                PersonaId = psicopedagogoId,
                Avatar = request.Avatar,

                Usuario = new Usuario
                {
                    CorreoElectronico = request.CorreoElectronico,
                    Contrasenia = contraseniaHasher.Hash(request.Contrasenia)
                }
            };

            await repository.UpdateAsync(psicopedagogo, ct);

            logger.LogInformation("Psicopedagogo con id {PsicopedagogoId} actualizado exitosamente.", psicopedagogoId);
            return Result<PsicopedagogoDto>.Success( PsicopedagogoDto.FromEntity(psicopedagogo));
        }
    }
}
