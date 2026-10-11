import { TipoDificultad } from "../../paciente.model";
import backendApi from "./backendApi";




export const obtenerTiposDificultad = async (token: string): Promise<TipoDificultad[]> => {
  try {
    const response = await backendApi(token).get("/api/tipos-dificultad");
    return response.data;
  } catch (error) {
    console.error("Error al obtener tipos de dificultad:", error);
    throw error;
  }
}