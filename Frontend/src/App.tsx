import { Route, Routes } from "react-router-dom";
import Pacientes from "./pages/Pacientes";
import Informacion from "./pages/paciente/informacion";
import AppLayout from "./layout/AppLayout";

function App() {
  return (
    <Routes>
      <Route element={<AppLayout />}>
        <Route index element={<Pacientes />} />
        <Route path="/pacientes/:id/" element={<Informacion />} />
      </Route>
    </Routes>
  );
}

export default App;
