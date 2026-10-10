/** Item de GET /api/pacientes (PacienteListItemDto). fechaNacimiento llega como "yyyy-MM-dd". */
export interface PacienteListItem {
  id: number;
  nombre: string;
  apellido: string;
  documento: string;
  fechaNacimiento: string;
  tiposDificultad: string[];
}

/** Respuesta paginada de GET /api/pacientes (PagedResponse<T>). */
export interface PagedResponse<T> {
  items: T[];
  total: number;
  page: number;
  pageSize: number;
}

/** Query params de GET /api/pacientes. */
export interface PacientesQuery {
  search?: string;
  tipoDificultadId?: number;
  page?: number;
  pageSize?: number;
}

export type PacientesResponse = PagedResponse<PacienteListItem>;
