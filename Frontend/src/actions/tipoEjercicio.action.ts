import { TipoEjercicio } from "../Model/paciente.model";
import { backendApi } from "./api/backendApi";

export const filtrarHistorialPacientePorTipoEjercicio = async (token: string) : Promise<TipoEjercicio[]> => {
try {
    const response = await backendApi(token).get(`/api/tipos-ejercicio/`)
      return response.data; 
  }catch (error) {  
    console.error("Error al filtrar historial del paciente por tipo de ejercicio:", error);
    throw error;
  }
}