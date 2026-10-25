

export interface CreatePsicopedagogoRequest{
    correoElectronico: string,
    contrasenia: string,
    contrasenia2?: string,
    avatar?: string,
    nombre: string,
    apellido: string,
    telefono: string,
    documento: string,
    matricula: string,
    genero: string,
    fechaNacimiento: string,
    aceptoTerminos: string | boolean
}

export interface User {
  id: number;
  name: string;
  role: string;
  token: string;
}
