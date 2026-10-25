import { backendApiLogin } from "./api/backendApi";

export const login = async (CorreoElectronico:string, Contrasenia:string) => {
    try {
        const response = await backendApiLogin().post('/api/auth/login', {
            CorreoElectronico,
            Contrasenia
        });
        return response.data;
    } catch (error) {
        console.error("Error al iniciar sesión:", error);
        throw error;
    }
}

export const ForgotPassword = async (correoElectronico:string) => {
    try {
        const response = await backendApiLogin().post(
                '/api/auth/forgot-password',
                JSON.stringify(correoElectronico)
            );
        return response.data;
    } catch (error) {
        console.error("Error al iniciar sesión:", error);
        throw error;
    }
}


export const ResetPassword = async (token:string, nuevaContrasenia:string) => {
    try {
        const response = await backendApiLogin().post(
            '/api/auth/reset-password',
            JSON.stringify({ token, nuevaContrasenia })
        );
        return response.data;
    } catch (error) {
        console.error("Error al restablecer la contraseña:", error);
        throw error;
    }
}
