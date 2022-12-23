import React from 'react';
import tw from 'tailwind-styled-components';

const Text = tw.div<StyleProps>`
  text-2xl 
  font-bold
  ${(props) =>
    props.text_color ? `text-${props.text_color}` : `text-zombieBlack`}
`;

interface StyleProps {
  text_color?: string;
}

export interface HeaderTextProps {
  text: string;
  text_color?: string;
}

const HeaderText: React.FC<HeaderTextProps> = ({ text, text_color }) => {
  return <Text text_color={text_color}>{text}</Text>;
};

export default HeaderText;
