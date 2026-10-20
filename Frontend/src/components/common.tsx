import { Stack, Box, Typography, Avatar, Pagination } from "@mui/material";
import React from "react";
import TrackChangesIcon from "@mui/icons-material/TrackChanges";
import memoriaIcon from "../assets/icons/memoria.png";
import calculoIcon from "../assets/icons/calculo.png";
import DrawIcon from "@mui/icons-material/Draw";
import AutoStoriesOutlinedIcon from "@mui/icons-material/AutoStoriesOutlined";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import dayjs from "dayjs";
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

const TYPE_ICONS: Record<string, React.ElementType | string> = {
  Atención: TrackChangesIcon,
  Memoria: memoriaIcon,
  Lectura: AutoStoriesOutlinedIcon,
  Escritura: DrawIcon,
  Cálculo: calculoIcon,
};

export function IconBox({
  icon,
  size,
}: {
  icon: React.ElementType | string;
  size: number;
}) {
  return (
    <Box
      className="rounded-lg grid place-items-center shrink-0"
      sx={{
        width: size,
        height: size,
        bgcolor: "rgba(37, 99, 235, 0.08)",
        color: "primary.main",
      }}
    >
      {typeof icon === "string" ? (
        <Box
          component="img"
          src={icon}
          alt=""
          className="w-[62%] h-[62%] object-contain"
        />
      ) : (
        React.createElement(icon)
      )}
    </Box>
  );
}

export function ExerciseTypeIcon({
  icon,
  size,
}: {
  icon: string;
  size: number;
}) {
  return <IconBox icon={TYPE_ICONS[icon] ?? TrackChangesIcon} size={size} />;
}

export function TablePager({
  page,
  count,
  onChange,
}: {
  page: number;
  count: number;
  onChange: (p: number) => void;
}) {
  return (
    <Stack direction="row" className="justify-end px-4 py-3">
      <Pagination
        count={count}
        page={page}
        onChange={(_, value) => onChange(value)}
        color="primary"
      />
    </Stack>
  );
}

export function CenteredPagination({
  page,
  count,
  onChange,
}: {
  page: number;
  count: number;
  onChange: (p: number) => void;
}) {
  return (
    <Stack direction="row" sx={{ justifyContent: "center" }}>
      <Pagination
        count={count}
        page={page}
        onChange={(_, value) => onChange(value)}
        color="primary"
      />
    </Stack>
  );
}

export function DateFilter({
  onChange,
  date,
  label = "Filtrar por fecha",
  width = 220,
}: {
  onChange: (date: Date | undefined) => void;
  date?: Date;
  label?: string;
  width?: number;
}) {
  return (
    <DatePicker
      label={label}
      value={date ? dayjs(date) : null}
      sx={{
        width: { xs: "100%", sm: width },
        bgcolor: "background.paper",
      }}
      format="DD/MM/YYYY"
      slotProps={{ field: { clearable: true }, textField: { size: "small" } }}
      onChange={(d) => onChange(d?.toDate())} // null → undefined, sin ?? new Date()
    />
  );
}
