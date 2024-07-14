import * as React from "react";
import { styled, useTheme, alpha } from "@mui/material/styles";
import Box from "@mui/material/Box";
import MuiDrawer from "@mui/material/Drawer";
import MuiAppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import List from "@mui/material/List";
import CssBaseline from "@mui/material/CssBaseline";
import Typography from "@mui/material/Typography";
import Divider from "@mui/material/Divider";
import IconButton from "@mui/material/IconButton";
import MenuIcon from "@mui/icons-material/Menu";
import ChevronLeftIcon from "@mui/icons-material/ChevronLeft";
import ChevronRightIcon from "@mui/icons-material/ChevronRight";
import ListItem from "@mui/material/ListItem";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import HomeIcon from "@mui/icons-material/Home";
import ImportContactsIcon from "@mui/icons-material/ImportContacts";
import TerminalIcon from "@mui/icons-material/Terminal";
import SchoolIcon from "@mui/icons-material/School";
import CalendarTodayIcon from "@mui/icons-material/CalendarToday";
import GroupIcon from "@mui/icons-material/Group";
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import FolderCopyIcon from "@mui/icons-material/FolderCopy";
import SettingsIcon from "@mui/icons-material/Settings";
import ScheduleOutlined from "@mui/icons-material/ScheduleOutlined";
import Button from "@mui/material/Button";
import Grid from "@mui/material/Unstable_Grid2";
import { AuthContext } from "../../context/AuthContext";
import { useContext } from "react";
import Menu from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import EditIcon from "@mui/icons-material/Edit";
import ArchiveIcon from "@mui/icons-material/Archive";
import FileCopyIcon from "@mui/icons-material/FileCopy";
import MoreHorizIcon from "@mui/icons-material/MoreHoriz";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import Dashboard from "../../views/Dashboard";
import About from "../../views/About";
import Syllabus from "../../views/Syllabus/Syllabus";
import { Link } from "react-router-dom";

const drawerWidth = 240;

const openedMixin = (theme) => ({
  width: drawerWidth,
  transition: theme.transitions.create("width", {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.enteringScreen,
  }),
  overflowX: "hidden",
});

const closedMixin = (theme) => ({
  transition: theme.transitions.create("width", {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  overflowX: "hidden",
  width: `calc(${theme.spacing(7)} + 1px)`,
  [theme.breakpoints.up("sm")]: {
    width: `calc(${theme.spacing(8)} + 1px)`,
  },
});

const DrawerHeader = styled("div")(({ theme }) => ({
  display: "flex",
  alignItems: "center",
  justifyContent: "flex-end",
  padding: theme.spacing(0, 1),
  // necessary for content to be below app bar
  ...theme.mixins.toolbar,
}));

const AppBar = styled(MuiAppBar, {
  shouldForwardProp: (prop) => prop !== "open",
})(({ theme, open }) => ({
  zIndex: theme.zIndex.drawer + 1,
  transition: theme.transitions.create(["width", "margin"], {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  ...(open && {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`,
    transition: theme.transitions.create(["width", "margin"], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  }),
}));

const StyledMenu = styled((props) => (
  <Menu
    elevation={0}
    anchorOrigin={{
      vertical: "bottom",
      horizontal: "right",
    }}
    transformOrigin={{
      vertical: "top",
      horizontal: "right",
    }}
    {...props}
  />
))(({ theme }) => ({
  "& .MuiPaper-root": {
    borderRadius: 6,
    marginTop: theme.spacing(1),
    minWidth: 180,
    color:
      theme.palette.mode === "light"
        ? "rgb(55, 65, 81)"
        : theme.palette.grey[300],
    boxShadow:
      "rgb(255, 255, 255) 0px 0px 0px 0px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px, rgba(0, 0, 0, 0.1) 0px 10px 15px -3px, rgba(0, 0, 0, 0.05) 0px 4px 6px -2px",
    "& .MuiMenu-list": {
      padding: "4px 0",
    },
    "& .MuiMenuItem-root": {
      "& .MuiSvgIcon-root": {
        fontSize: 18,
        color: theme.palette.text.secondary,
        marginRight: theme.spacing(1.5),
      },
      "&:active": {
        backgroundColor: alpha(
          theme.palette.primary.main,
          theme.palette.action.selectedOpacity
        ),
      },
    },
  },
}));

const Drawer = styled(MuiDrawer, {
  shouldForwardProp: (prop) => prop !== "open",
})(({ theme, open }) => ({
  width: drawerWidth,
  flexShrink: 0,
  whiteSpace: "nowrap",
  boxSizing: "border-box",
  ...(open && {
    ...openedMixin(theme),
    "& .MuiDrawer-paper": openedMixin(theme),
  }),
  ...(!open && {
    ...closedMixin(theme),
    "& .MuiDrawer-paper": closedMixin(theme),
  }),
}));

export default function SideBarMenu() {
  const theme = useTheme();
  const [open, setOpen] = React.useState(false);
  const [syllabusDropMenu, setSyllabusDropMenu] = React.useState(null);
  const [trainingProgramDropMenu, setTrainingProgramDropMenu] =
    React.useState(null);
  const [trainingClassDropMenu, setTrainingClassDropMenu] =
    React.useState(null);

  const [menuData, setMenuData] = React.useState("Home");

  const openMenu = Boolean(syllabusDropMenu);
  const openMenuTrainingProgram = Boolean(trainingProgramDropMenu);
  const openMenuTrainingClass = Boolean(trainingClassDropMenu);

  const {
    authState: {
      user
    },
    logoutUser,
  } = useContext(AuthContext);

  const handleClickSyllabus = (event) => {
    setSyllabusDropMenu(event.currentTarget);
  };

  const handleClickTrainingProgram = (event) => {
    setTrainingProgramDropMenu(event.currentTarget);
  };
  const handleClickTrainingClass = (event) => {
    setTrainingClassDropMenu(event.currentTarget);
  };
  const handleClose = () => {
    setSyllabusDropMenu(null);
  };

  const handleCloseTrainingProgram = () => {
    setTrainingProgramDropMenu(null);
  };
  const handleCloseTrainingClass = () => {
    setTrainingClassDropMenu(null);
  };
  const handleDrawerOpen = () => {
    setOpen(true);
  };

  const handleDrawerClose = () => {
    setOpen(false);
  };
  const logout = () => logoutUser();

  return (
    <Box sx={{ display: "flex" }}>
      <CssBaseline />
      <AppBar position="fixed" open={open} sx={{ backgroundColor: "#2D3748" }}>
        <Toolbar>
          <IconButton
            color="inherit"
            aria-label="open drawer"
            onClick={handleDrawerOpen}
            edge="start"
            sx={{
              marginRight: 5,
              ...(open && { display: "none" }),
            }}
          >
            <MenuIcon />
          </IconButton>
          <Grid
            xs={12}
            container
            justifyContent="space-between"
            alignItems="center"
            flexDirection={{ xs: "column", sm: "row" }}
            sx={{ fontSize: "12px" }}
          >
            <img src="https://yt3.googleusercontent.com/ytc/AGIKgqPHdohWib-LdYpcBEV7J6ExBTZudwIhZYsFnQQrvg=s176-c-k-c0x00ffffff-no-rj" alt="System Manager Icon" style={{ width: '24px', marginRight: '8px' }} />
            <Typography variant="h6" noWrap component="div">
              System Manager Freshers
            </Typography>
            <Button
              className="font-weight-bolder text-white"
              variant="contained"
              href="#"
              sx={{
                backgroundColor: "#FF6600",
                '&:hover': {
                  backgroundColor: "#3E4C59"
                },
              }
              }
              onClick={logout}
            >
              Logout
            </Button>
          </Grid>
        </Toolbar>
      </AppBar>
      <Drawer variant="permanent" open={open}>
        <DrawerHeader>
          <IconButton onClick={handleDrawerClose}>
            {theme.direction === "rtl" ? (
              <ChevronRightIcon />
            ) : (
              <ChevronLeftIcon />
            )}
          </IconButton>
        </DrawerHeader>
        <Divider />
        <List>
          {/* home */}
          <ListItem disablePadding sx={{ display: "block" }}>
            <ListItemButton
              id="btn-syllabus"
              sx={{
                minHeight: 48,
                justifyContent: open ? "initial" : "center",
                px: 2.5,
              }}
              to="/"
              as={Link}
            >
              <ListItemIcon
                sx={{
                  minWidth: 0,
                  mr: open ? 3 : "auto",
                  justifyContent: "center",
                }}
              >
                <HomeIcon />
              </ListItemIcon>
              <ListItemText primary={"Home"} sx={{ opacity: open ? 1 : 0 }} />
            </ListItemButton>
          </ListItem>
          {/* syllabus */}
          <ListItem disablePadding sx={{ display: "block" }}>
            <ListItemButton
              sx={{
                minHeight: 48,
                justifyContent: open ? "initial" : "center",
                px: 2.5,
              }}
              //onClick={()=> setMenuData("Syllabus")}
              onClick={handleClickSyllabus}
              aria-controls={openMenu ? "btn-syllabus" : undefined}
              aria-haspopup="true"
              aria-expanded={openMenu ? "true" : undefined}
            >
              <ListItemIcon
                sx={{
                  minWidth: 0,
                  mr: open ? 3 : "auto",
                  justifyContent: "center",
                }}
              >
                <ImportContactsIcon />
              </ListItemIcon>
              <ListItemText
                primary={"Syllabus"}
                sx={{ opacity: open ? 1 : 0 }}
              />
            </ListItemButton>
            <StyledMenu
              id="btn-syllabus"
              MenuListProps={{
                "aria-labelledby": "btn-syllabus",
              }}
              anchorEl={syllabusDropMenu}
              open={openMenu}
              onClick={handleClose}
            >
              <MenuItem onClick={handleClose} to="/ViewSyllabus" as={Link}>
                <EditIcon />
                View Syllabus
              </MenuItem>
              <MenuItem
                onClick={handleClose}
                disableRipple
                to="/Syllabus"
                as={Link}
              >
                <FileCopyIcon />
                Create Syllabus
              </MenuItem>
            </StyledMenu>
          </ListItem>
          {/* TrainingProgram */}
          <ListItem disablePadding sx={{ display: "block" }}>
            <ListItemButton
              onClick={handleClickTrainingProgram}
              sx={{
                minHeight: 48,
                justifyContent: open ? "initial" : "center",
                px: 2.5,
              }}
            >
              <ListItemIcon
                sx={{
                  minWidth: 0,
                  mr: open ? 3 : "auto",
                  justifyContent: "center",
                }}
              >
                <TerminalIcon />
              </ListItemIcon>
              <ListItemText
                primary={"TrainingProgram"}
                sx={{ opacity: open ? 1 : 0 }}
              />
            </ListItemButton>
            <StyledMenu
              id="btn-syllabus"
              MenuListProps={{
                "aria-labelledby": "btn-syllabus",
              }}
              anchorEl={trainingProgramDropMenu}
              open={openMenuTrainingProgram}
              onClick={handleCloseTrainingProgram}
            >
              <MenuItem
                onClick={handleCloseTrainingProgram}
                to="/TrainingProgram"
                as={Link}
              >
                <EditIcon />
                View Training Program
              </MenuItem>
              <MenuItem
                onClick={handleCloseTrainingProgram}
                disableRipple
                to="/CreateTrainingProgram"
                as={Link}
              >
                <FileCopyIcon />
                Create Training Program
              </MenuItem>
            </StyledMenu>
          </ListItem>
          {/* TrainingClass */}
          <ListItem disablePadding sx={{ display: "block" }}>
            <ListItemButton
              onClick={handleClickTrainingClass}
              sx={{
                minHeight: 48,
                justifyContent: open ? "initial" : "center",
                px: 2.5,
              }}
            >
              <ListItemIcon
                sx={{
                  minWidth: 0,
                  mr: open ? 3 : "auto",
                  justifyContent: "center",
                }}
              >
                <SchoolIcon />
              </ListItemIcon>
              <ListItemText
                primary={"TrainingClass"}
                sx={{ opacity: open ? 1 : 0 }}
              />
            </ListItemButton>
            <StyledMenu
              id="btn-syllabus"
              MenuListProps={{
                "aria-labelledby": "btn-syllabus",
              }}
              anchorEl={trainingClassDropMenu}
              open={openMenuTrainingClass}
              onClick={handleCloseTrainingClass}
            >
              <MenuItem
                onClick={handleCloseTrainingClass}
                to="/TrainingClass"
                as={Link}
              >
                <EditIcon />
                View Training Class
              </MenuItem>
              <MenuItem
                onClick={handleCloseTrainingClass}
                disableRipple
                to="/CreateTrainingClass"
                as={Link}
              >
                <SchoolIcon />
                Create Training Class
              </MenuItem>
            </StyledMenu>
          </ListItem>
          <ListItem disablePadding sx={{ display: "block" }}>
            <ListItemButton
              sx={{
                minHeight: 48,
                justifyContent: open ? "initial" : "center",
                px: 2.5,
              }}
            >
              <ListItemIcon
                sx={{
                  minWidth: 0,
                  mr: open ? 3 : "auto",
                  justifyContent: "center",
                }}
                to="Attendance"
                as={Link}
              >
                <CalendarTodayIcon />
              </ListItemIcon>
              <ListItemText
                primary={"Attendance"}
                sx={{ opacity: open ? 1 : 0 }}
              />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding sx={{ display: "block" }}>
            <ListItemButton
              sx={{
                minHeight: 48,
                justifyContent: open ? "initial" : "center",
                px: 2.5,
              }}
              to="User"
              as={Link}
            >
              <ListItemIcon
                sx={{
                  minWidth: 0,
                  mr: open ? 3 : "auto",
                  justifyContent: "center",
                }}
              >
                <GroupIcon />
              </ListItemIcon>
              <ListItemText
                primary={"UserManagement"}
                sx={{ opacity: open ? 1 : 0 }}
              />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding sx={{ display: "block" }}>
            <ListItemButton
              sx={{
                minHeight: 48,
                justifyContent: open ? "initial" : "center",
                px: 2.5,
              }}
              to="User/Profile"
              as={Link}
            >
              <ListItemIcon
                sx={{
                  minWidth: 0,
                  mr: open ? 3 : "auto",
                  justifyContent: "center",
                }}
              >
                <AccountCircleIcon />
              </ListItemIcon>
              <ListItemText
                primary={"UserProfile"}
                sx={{ opacity: open ? 1 : 0 }}
              />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding sx={{ display: "block" }}>
            <ListItemButton
              sx={{
                minHeight: 48,
                justifyContent: open ? "initial" : "center",
                px: 2.5,
              }}
            >
              <ListItemIcon
                sx={{
                  minWidth: 0,
                  mr: open ? 3 : "auto",
                  justifyContent: "center",
                }}
              >
                <FolderCopyIcon />
              </ListItemIcon>
              <ListItemText
                primary={"Learning material"}
                sx={{ opacity: open ? 1 : 0 }}
              />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding sx={{ display: "block" }}>
            <ListItemButton
              sx={{
                minHeight: 48,
                justifyContent: open ? "initial" : "center",
                px: 2.5,
              }}
            >
              <ListItemIcon
                sx={{
                  minWidth: 0,
                  mr: open ? 3 : "auto",
                  justifyContent: "center",
                }}
              >
                <SettingsIcon />
              </ListItemIcon>
              <ListItemText
                primary={"Setting"}
                sx={{ opacity: open ? 1 : 0 }}
              />
            </ListItemButton>
          </ListItem>

          {/* {['Inbox', 'Starred', 'Send email', 'Drafts'].map((text, index) => (
            <ListItem key={text} disablePadding sx={{ display: 'block' }}>
              <ListItemButton
                sx={{
                  minHeight: 48,
                  justifyContent: open ? 'initial' : 'center',
                  px: 2.5,
                }}
              >
                <ListItemIcon
                  sx={{
                    minWidth: 0,
                    mr: open ? 3 : 'auto',
                    justifyContent: 'center',
                  }}
                >
                  {index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
                </ListItemIcon>
                <ListItemText primary={text} sx={{ opacity: open ? 1 : 0 }} />
              </ListItemButton>
            </ListItem>
          ))} */}
        </List>
        {/* <Divider />
        <List>
          {['All mail', 'Trash', 'Spam'].map((text, index) => (
            <ListItem key={text} disablePadding sx={{ display: 'block' }}>
              <ListItemButton
                sx={{
                  minHeight: 48,
                  justifyContent: open ? 'initial' : 'center',
                  px: 2.5,
                }}
              >
                <ListItemIcon
                  sx={{
                    minWidth: 0,
                    mr: open ? 3 : 'auto',
                    justifyContent: 'center',
                  }}
                >
                  {index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
                </ListItemIcon>
                <ListItemText primary={text} sx={{ opacity: open ? 1 : 0 }} />
              </ListItemButton>
            </ListItem>
          ))}
        </List> */}
      </Drawer>
      <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
        {menuData == "Syllabus" && <Syllabus />}
      </Box>
    </Box>
  );
}
