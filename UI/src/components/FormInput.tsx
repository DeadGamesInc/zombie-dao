import React from 'react';
import tw from 'tailwind-styled-components';

import InputText from 'components/InputText';
import InputField from 'components/InputField';

const InputContainer = tw.div`
  flex 
  w-full
`;

export interface FormInputProps {
  text: string;
  type: string;
  name: string;
  defaultValue?: string;
}

const FormInput: React.FC<FormInputProps> = ({
  text,
  type,
  name,
  defaultValue,
}) => {
  return (
    <InputContainer>
      <InputText text={text} />
      <InputField type={type} name={name} defaultValue={defaultValue} />
    </InputContainer>
  );
};

export default FormInput;
