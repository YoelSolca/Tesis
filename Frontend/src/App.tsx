import { useEffect, useState } from "react";
import "./App.css";
import "./index.css";
import Persona from "./personas.model";
function App() {
  const [personas, setPersonas] = useState<Persona[]>();

  useEffect(() => {
    fetch(`${import.meta.env.VITE_API_URL}/api/personas`)
      .then((response) => response.json())
      .then((data) => setPersonas(data));
  }, []);

  return (
    <>
      {personas ? (
        <div>
          <ul>
            {personas.map((p) => (
              <li key={p.id}>
                {p.nombre} {p.apellido} - {p.telefono} - {p.documento} -{" "}
                {p.genero} - {p.fechaNacimiento}
              </li>
            ))}
          </ul>
        </div>
      ) : (
        "cargando..."
      )}
    </>
  );
}

export default App;
