import { PacientesResponse } from "../paciente.model";
import { PacienteDetalle } from "../PacienteDetalle.model";
import backendApi from "./api/backendApi";

export const obtenerPacientes = async (token: string, search: string, tipoDificultadId?: number): Promise<PacientesResponse> => {
  try {
    const response = await backendApi(token).get("/api/pacientes", {
        params: {
           search: search,
           page: 1,
           pageSize: 10,
           tipoDificultadId: tipoDificultadId,
        },
    });

    return response.data;
  } catch (error) {
    console.error("Error al obtener pacientes:", error);
    throw error;
  }
}

export const obtenerPacientePorId = async (token: string, id: string) => {
  try {
    const response = await backendApi(token).get(`/api/pacientes/${id}`);

    return response.data as Promise<PacienteDetalle>;
  } catch (error) {
    console.error("Error al obtener paciente por ID:", error);
    throw error;
  }
}

export const guardarCambiosPaciente = async (token: string, id: string, paciente: PacienteDetalle) => {
  try {
    const response = await backendApi(token).put(`/api/pacientes/${id}`, paciente);
    return response.data;
  } catch (error) {
    console.error("Error al guardar cambios del paciente:", error);
    throw error;
  }
};