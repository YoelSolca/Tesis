import { CreatePsicopedagogoRequest } from "../Model/psicopedagogo.model";
import { backendApiLogin } from "./api/backendApi";


export const register = async (request: CreatePsicopedagogoRequest)  =>{
    try{    
        const { contrasenia2, aceptoTerminos, genero, ...rest } = request;
        const response = await backendApiLogin().post('/api/psicopedagogo/',
            { ...rest, genero: genero.charAt(0), aceptoTerminos: Boolean(aceptoTerminos) }
        );

        return response.data;
    }catch (error){
        console.error("Error al registrarse:", error);
        throw error; 
    }

}