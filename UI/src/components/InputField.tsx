import React from 'react';
import tw from 'tailwind-styled-components';

const Field = tw.input`
  ml-2 
  border-4 
  border-opacity-100 
  w-96
`;

export interface InputFieldProps {
  type: string;
  name: string;
  defaultValue?: string;
}

const InputField: React.FC<InputFieldProps> = ({
  type,
  name,
  defaultValue,
}) => {
  return <Field type={type} name={name} defaultValue={defaultValue} />;
};

export default InputField;
