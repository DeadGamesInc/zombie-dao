import React from 'react';
import tw from 'tailwind-styled-components';

const Text = tw.div<StyleProps>`
  text-lg 
  m-2
  ${(props) =>
    props.text_color ? `text-${props.text_color}` : `text-zombieBlack`}
`;

interface StyleProps {
  text_color?: string;
}

export interface ParagraphTextProps {
  text: string;
  text_color?: string;
}

const ParagraphText: React.FC<ParagraphTextProps> = ({ text, text_color }) => {
  return <Text text_color={text_color}>{text}</Text>;
};

export default ParagraphText;
