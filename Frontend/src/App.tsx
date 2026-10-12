import { Route, Routes } from "react-router-dom";
import Pacientes from "./pages/Pacientes";
import AppLayout from "./layout/AppLayout";
import PacienteLayout from "./layout/PacienteLayout";
import Historial from "./pages/paciente/Historial";
import Informacion from "./pages/paciente/Informacion";

function App() {
  return (
    <Routes>
      <Route element={<AppLayout />}>
        <Route index element={<Pacientes />} />
        <Route path="/pacientes/:id" element={<PacienteLayout />}>
          <Route path="informacion" element={<Informacion />} />
          <Route path="historial" element={<Historial />} />
        </Route>
      </Route>
    </Routes>
  );
}

export default App;
