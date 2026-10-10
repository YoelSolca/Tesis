import { Stack, Box, Typography, Card, Tabs, Tab, Avatar } from "@mui/material";
import React from "react";
import { PacienteDetalle } from "../PacienteDetalle.model";
import { formatearEdad } from "../utils/edad";

export function PersonAvatar({
  initials,
  size = 40,
}: {
  initials: string;
  size?: number;
}) {
  const [bg, fg] = ["#E6DCF7", "#6B3FA0"];
  const fontSize = size <= 32 ? 14 : size <= 40 ? 16 : 22;
  return (
    <Avatar
      sx={{
        width: size,
        height: size,
        bgcolor: bg,
        color: fg,
        fontSize,
        fontWeight: 500,
      }}
    >
      {initials}
    </Avatar>
  );
}

// este componente se encarga de mostrar la cabecera de las página
//  También puede incluir un botón de acción opcional.
export function PageHeader({
  title,
  subtitle,
  action,
}: {
  title: string;
  subtitle?: string;
  action?: React.ReactNode;
}) {
  return (
    <Stack
      direction="row"
      useFlexGap
      className="gap-4 flex-wrap items-end justify-between"
    >
      <Box>
        <Typography variant="h4" component="h1">
          {title}
        </Typography>
        {subtitle && (
          <Typography variant="body1" color="text.secondary" className="mt-1">
            {subtitle}
          </Typography>
        )}
      </Box>
      {action && <Box>{action}</Box>}
    </Stack>
  );
}

// este componente se encarga de mostrar la cabecera de la página de pacientes
export function PatientHeader({
  patient,
  action,
}: {
  patient: PacienteDetalle;
  action?: React.ReactNode;
}) {
  return (
    <Card className="p-6" key={patient.id}>
      <Stack
        direction="row"
        useFlexGap
        className="gap-4 flex-wrap items-center"
      >
        <PersonAvatar
          initials={patient.nombre.charAt(0) + patient.apellido.charAt(0)}
          size={56}
        />
        <Box className="flex-1 min-w-40">
          <Typography variant="h5" component="h1">
            {patient.nombre} {patient.apellido}
          </Typography>
          <Typography variant="body2" color="text.secondary">
            {formatearEdad(patient.fechaNacimiento)} · DNI{" "}
            {Number(patient.documento).toLocaleString("es-AR")}
          </Typography>
        </Box>
        {action}
      </Stack>
    </Card>
  );
}

const PATIENT_TABS = ["Información", "Historial", "Evolución", "Informes"];

export function PatientTabs({ value }: { value: number }) {
  return (
    <Tabs
      value={value}
      variant="scrollable"
      allowScrollButtonsMobile
      sx={{ borderBottom: 1, borderColor: "divider" }}
    >
      {PATIENT_TABS.map((t, i) => (
        <Tab key={i} label={t} />
      ))}
    </Tabs>
  );
}

export function SectionHeader({
  title,
  subtitle,
  action,
}: {
  title: string;
  subtitle?: string;
  action?: React.ReactNode;
}) {
  return (
    <Stack direction="row" spacing={2} className="items-center justify-between">
      <Box>
        <Typography variant="subtitle1" component="h2">
          {title}
        </Typography>
        {subtitle && (
          <Typography variant="body2" color="text.secondary">
            {subtitle}
          </Typography>
        )}
      </Box>
      {action}
    </Stack>
  );
}

export function KeyValue({ label, value }: { label: string; value: string }) {
  return (
    <Box>
      <Typography variant="caption" color="text.secondary" className="block">
        {label}
      </Typography>
      <Typography variant="body1">{value}</Typography>
    </Box>
  );
}
