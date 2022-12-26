import React from 'react';
import { useRoutes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import './custom.css';

export const App = () => {
  const element = useRoutes(AppRoutes);
  return (
    <Layout>
      {element}
    </Layout>
  );
};