import { styled } from "@mui/material/styles";
import Box from "@mui/material/Box";

export const CenteredColumn = styled(Box)(() => ({
  display: "flex",
  flexDirection: "column",
  alignItems: "center",
  textAlign: "center",
}));
