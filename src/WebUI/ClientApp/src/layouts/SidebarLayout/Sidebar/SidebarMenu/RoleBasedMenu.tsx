import {
  ListSubheader,
  alpha,
  Box,
  List,
  styled
} from '@mui/material';
import { connect } from 'react-redux';
import TableChartTwoToneIcon from '@mui/icons-material/TableChartTwoTone';
import AdminPanelSettingsIcon from '@mui/icons-material/AdminPanelSettings';
import HomeIcon from '@mui/icons-material/Home';

import UserService from 'src/services/UserService';

import MenuItem from './MenuItem';


const SubMenuWrapper = styled(Box)(
  ({ theme }) => `
    .MuiList-root {

      .MuiListItem-root {
        padding: 1px 0;

        .MuiBadge-root {
          position: absolute;
          right: ${theme.spacing(3.2)};

          .MuiBadge-standard {
            background: ${theme.colors.primary.main};
            font-size: ${theme.typography.pxToRem(10)};
            font-weight: bold;
            text-transform: uppercase;
            color: ${theme.palette.primary.contrastText};
          }
        }
    
        .MuiButton-root {
          display: flex;
          color: ${theme.colors.alpha.trueWhite[70]};
          background-color: transparent;
          width: 100%;
          justify-content: flex-start;
          padding: ${theme.spacing(1.2, 3)};

          .MuiButton-startIcon,
          .MuiButton-endIcon {
            transition: ${theme.transitions.create(['color'])};

            .MuiSvgIcon-root {
              font-size: inherit;
              transition: none;
            }
          }

          .MuiButton-startIcon {
            color: ${theme.colors.alpha.trueWhite[30]};
            font-size: ${theme.typography.pxToRem(20)};
            margin-right: ${theme.spacing(1)};
          }
          
          .MuiButton-endIcon {
            color: ${theme.colors.alpha.trueWhite[50]};
            margin-left: auto;
            opacity: .8;
            font-size: ${theme.typography.pxToRem(20)};
          }

          &.active,
          &:hover {
            background-color: ${alpha(theme.colors.alpha.trueWhite[100], 0.06)};
            color: ${theme.colors.alpha.trueWhite[100]};

            .MuiButton-startIcon,
            .MuiButton-endIcon {
              color: ${theme.colors.alpha.trueWhite[100]};
            }
          }
        }

        &.Mui-children {
          flex-direction: column;

          .MuiBadge-root {
            position: absolute;
            right: ${theme.spacing(7)};
          }
        }

        .MuiCollapse-root {
          width: 100%;

          .MuiList-root {
            padding: ${theme.spacing(1, 0)};
          }

          .MuiListItem-root {
            padding: 1px 0;

            .MuiButton-root {
              padding: ${theme.spacing(0.8, 3)};

              .MuiBadge-root {
                right: ${theme.spacing(3.2)};
              }

              &:before {
                content: ' ';
                background: ${theme.colors.alpha.trueWhite[100]};
                opacity: 0;
                transition: ${theme.transitions.create([
    'transform',
    'opacity'
  ])};
                width: 6px;
                height: 6px;
                transform: scale(0);
                transform-origin: center;
                border-radius: 20px;
                margin-right: ${theme.spacing(1.8)};
              }

              &.active,
              &:hover {

                &:before {
                  transform: scale(1);
                  opacity: 1;
                }
              }
            }
          }
        }
      }
    }
`
);

const RoleBasedMenu = (props: any) => {

  const RenderMenu = () => {
    let result = [];

    switch (props.role) {
      case "Super Admin":
        result.push(
          <List
            key={"superadmin"}
            component="div"
            subheader={
              <ListSubheader component="div" disableSticky>
                Super Admin Menu
              </ListSubheader>
            }
          >
            <SubMenuWrapper>
              <List component="div">
                <MenuItem to="/configuration" icon={<AdminPanelSettingsIcon />} text="Configuration" />
              </List>
            </SubMenuWrapper>
          </List>
        )
      case "Admin":
        result.push(
          <List
            key={"admin"}
            component="div"
            subheader={
              <ListSubheader component="div" disableSticky>
                Users
              </ListSubheader>
            }
          >
            <SubMenuWrapper>
              <List component="div">
                <MenuItem to="/user" icon={<TableChartTwoToneIcon />} text="User List" />
              </List>
            </SubMenuWrapper>
          </List>
        )
      default:
        result.push(
          <List
            key={"user"}
            component="div"
            subheader={
              <ListSubheader component="div" disableSticky>
                Home
              </ListSubheader>
            }
          >
            <SubMenuWrapper>
              <List component="div">
                <MenuItem to="/home" icon={<HomeIcon />} text="Home page" />
              </List>
            </SubMenuWrapper>
          </List>
        )
        break;
    }

    return result;
  }

  return (
    <>
      {RenderMenu()}
    </>
  );
}

const mapStateToProps = (state: any) => {
  return {
    role: UserService.GetHighestRole(state.auth.user.roles)
  };
};

export default
  connect(mapStateToProps)
    (RoleBasedMenu);
