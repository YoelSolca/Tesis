import { Alert, Box, Button, CircularProgress, Collapse, Stack, TextField, Typography } from "@mui/material";
import { useState } from "react";
import PasswordField from "../../components/passwordField";
import { Link as RouterLink, useLocation, useNavigate } from 'react-router-dom';
import { login } from "../../actions/auth.action";
import axios from "axios";
import { useAuth } from "../../App.tsx";

export default function Login() {
  const [form, setForm] = useState({ email: "", password: "" });
  const [errors, setErrors] = useState({ email: "", password: "" });
  const isEmail = (v: string) => /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/.test(v);
  const [alert, setAlert] = useState<{ severity: "error" | "warning" | "info" | "success"; message: string } | null>(null);
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();
  const { signIn } = useAuth();
  const { state } = useLocation();



  const set =
    (k: "email" | "password") => (e: React.ChangeEvent<HTMLInputElement>) => {
      setForm((f) => ({ ...f, [k]: e.target.value }));
      setErrors((er) => ({ ...er, [k]: undefined }));
      setAlert(null);
    };
    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
      e.preventDefault();
      const er = { email: "", password: "" };
      if (!isEmail(form.email)) er.email = "Ingresá un correo electrónico válido";
      if (!form.password) er.password = "Ingresá una contraseña";
      if (er.email || er.password) {
        setErrors(er);
        return;
      }
    
      setLoading(true);
      setTimeout(async () => {
        try {
          const res = await login(form.email, form.password);
          setLoading(false);

          if(res.estado === 0) {
            setAlert({ severity: "info", message: "Tu solicitud todavía está pendiente de aprobación. Te avisaremos cuando un administrador la revise." });
            return;
          }

          signIn({ id: res.psicopedagogoId, name: res.psicopedagogoName, role: res.rol, token: res.token });
          navigate(state?.from ?? "/", { replace: true });

        } catch (error) {
          const detail = axios.isAxiosError(error)
          ? error.response?.data?.detail
          : undefined;
          setAlert({ severity: "error", message: detail ?? "No se pudo iniciar sesión. Intentá de nuevo." });     
         } finally {
            setLoading(false);
          }
        }, 600);
      };
        
  return (
    <Box
      component="form"
      noValidate
      onSubmit={handleSubmit}
      className="w-full max-w-[460px] mx-auto flex flex-col gap-4"
    >
      <Box className="flex flex-col gap-1 items-center mb-5">
        <Typography variant="h4" component="h1">
          Iniciar sesión
        </Typography>
        <Typography variant="body1" color="text.secondary" className="mt-1">
          Accedé a tu cuenta para continuar
        </Typography>
      </Box>

      <Collapse in={Boolean(alert)}>
        {alert && <Alert severity={alert.severity}>{alert.message}</Alert>}
      </Collapse>

      <TextField
        fullWidth
        autoFocus
        type="email"
        label="Correo electrónico"
        value={form.email}
        onChange={set("email")}
        error={Boolean(errors.email)}
        helperText={errors.email}
      />
       <PasswordField label="Contraseña" value={form.password} onChange={set('password')} error={Boolean(errors.password)} helperText={errors.password} />
       <Stack className="items-center gap-3 mt-2">
        <Button component={RouterLink} to="/recuperar" size="small" className="font-bold uppercase tracking-[0.3px]">¿Olvidaste tu contraseña?</Button>
        <Button type="submit" variant="contained" disabled={loading} className="min-w-[140px]">
          {loading ? <CircularProgress size={22} color="inherit" /> : 'Ingresar'}
        </Button>
      </Stack>
    </Box>
  );
}
