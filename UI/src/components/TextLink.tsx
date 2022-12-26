import React from 'react';
import { Link } from 'react-router-dom';

export interface TextLinkProps {
  text: string;
  target: string;
}

const TextLink: React.FC<TextLinkProps> = ({ text, target }) => {
  return (
    <Link to={target} className="text-zombieBlack hover:text-zombieRed">
      {text}
    </Link>
  );
};

export default TextLink;
