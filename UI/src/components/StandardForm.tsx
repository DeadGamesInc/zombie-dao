import React from 'react';

export interface StandardFormProps {
  children?: React.ReactNode;
  onSubmit: (event: any) => Promise<void>;
}

const StandardForm: React.FC<StandardFormProps> = ({ children, onSubmit }) => {
  return <form onSubmit={onSubmit}>{children}</form>;
};

export default StandardForm;
