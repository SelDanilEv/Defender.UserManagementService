import {
  Box,
  styled
} from '@mui/material';

import RoleBasedMenu from './RoleBasedMenu';


const MenuWrapper = styled(Box)(
  ({ theme }) => `
  .MuiList-root {
    padding: ${theme.spacing(1)};

    & > .MuiList-root {
      padding: 0 ${theme.spacing(0)} ${theme.spacing(1)};
    }
  }

    .MuiListSubheader-root {
      text-transform: uppercase;
      font-weight: bold;
      font-size: ${theme.typography.pxToRem(15)};
      color: ${theme.colors.alpha.trueWhite[50]};
      padding: ${theme.spacing(0, 2.5)};
      line-height: 1.4;
    }
`
);

const SidebarMenu = () => {
  return (
    <>
      <MenuWrapper>
        <Box height={25} />
        <RoleBasedMenu />
      </MenuWrapper>
    </>
  );
}

export default SidebarMenu;
