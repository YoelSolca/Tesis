import { useEffect, useState } from "react";
import type { PacienteListItem } from "../paciente.model";
import {
  Button,
  TableContainer,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Table,
  Card,
  Chip,
  Stack,
  TextField,
  InputAdornment,
  MenuItem,
} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { formatearEdad } from "../utils/edad";
import { useNavigate } from "react-router-dom";
import { PageHeader } from "../components/common";
import SearchIcon from "@mui/icons-material/Search";
import { obtenerPacientes } from "../actions/pacientes.action";
function Pacientes() {
  const [pacientes, setPacientes] = useState<PacienteListItem[]>([]);
  const navigate = useNavigate();
  const [search, setSearch] = useState("");

  // Estado para almacenar el valor de búsqueda con debounce
  //debouncedSearch se actualiza después de un retraso de 300 ms desde la última vez que el usuario escribió en el campo de búsqueda.
  //setDebouncedSearch se utiliza para actualizar el estado de debouncedSearch después del retraso.
  const [debouncedSearch, setDebouncedSearch] = useState("");

  const difficulties = [
    "Dificultades de atención",
    "Dificultades en lectoescritura",
    "Dificultades en cálculo",
    "Dificultades en memoria",
  ];
  const token = "";

  //Se ejecuta cada vez que el valor de search cambia, y establece un temporizador para actualizar debouncedSearch después de 300 ms.
  // Esto ayuda a reducir la cantidad de solicitudes de búsqueda mientras el usuario está escribiendo.
  useEffect(() => {
    const timer = setTimeout(() => setDebouncedSearch(search), 300);
    return () => clearTimeout(timer);
  }, [search]);

  useEffect(() => {
    obtenerPacientes(token, debouncedSearch)
      .then((response) => {
        setPacientes(response.items);
      })
      .catch((error) => {
        console.error("Error al obtener pacientes:", error);
      });
  }, [debouncedSearch]);
  //Se ejecuta cada vez que debouncedSearch cambia, y realiza una solicitud de búsqueda a la API para obtener los pacientes que coinciden con el término de búsqueda.

  return (
    <>
      <PageHeader
        title="Pacientes"
        subtitle="Lista de pacientes a tu cargo"
        action={
          <Button
            variant="contained"
            startIcon={<AddIcon />}
            onClick={() => navigate("/pacientes/nuevo")}
          >
            Nuevo paciente
          </Button>
        }
      />

      <Stack direction="row" useFlexGap className="gap-4 flex-wrap">
        <TextField
          size="small"
          placeholder="Buscar por nombre o documento"
          className="w-full sm:w-[360px] bg-white"
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          slotProps={{
            input: {
              startAdornment: (
                <InputAdornment position="start">
                  <SearchIcon fontSize="small" />
                </InputAdornment>
              ),
            },
          }}
        />
        <TextField
          select
          size="small"
          label="Tipo de dificultad"
          value=""
          className="w-full sm:w-[240px] bg-white"
        >
          <MenuItem value="Todas">Todas</MenuItem>
          {difficulties.map((d) => (
            <MenuItem key={d} value={d}>
              {d}
            </MenuItem>
          ))}
        </TextField>
      </Stack>

      <TableContainer component={Card}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>Nombre y apellido</TableCell>
              <TableCell>Número de documento</TableCell>
              <TableCell>Edad</TableCell>
              <TableCell>Tipo de dificultad</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {pacientes.map((p) => (
              <TableRow
                key={p.id}
                hover
                className="cursor-pointer"
                onClick={() => navigate(`/pacientes/${p.id}`)}
              >
                <TableCell component="th" scope="row">
                  {p.nombre} {p.apellido}
                </TableCell>
                <TableCell>{p.documento}</TableCell>
                <TableCell>{formatearEdad(p.fechaNacimiento)}</TableCell>
                <TableCell>
                  {p.tiposDificultad.map((t) => (
                    <Chip key={t} label={t} size="small" variant="outlined" />
                  ))}
                </TableCell>
              </TableRow>
            ))}

            {pacientes.length === 0 && (
              <TableRow>
                <TableCell colSpan={4} align="center">
                  No se encontraron pacientes.
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>
    </>
  );
}

export default Pacientes;
