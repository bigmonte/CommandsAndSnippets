import React from 'react';
import { Container } from 'reactstrap';
import NavMenu from "./NavMenu";

export const Layout = (props) => {
  const { children } = props;
  return (
    <div>
      <NavMenu/>
      <Container className="responsive max" tag="main">
        {children}
      </Container>
    </div>
  );
};