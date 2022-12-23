import React, { ReactNode } from 'react';
import tw from 'tailwind-styled-components';

import FooterBrand from 'components/FooterBrand';
import FooterCopyright from 'components/FooterCopyright';
import FooterCenter from 'components/FooterCenter';

const FooterWrapper = tw.div`
  relative 
  bg-zombieBlack 
  p-4 
  mt-6 
  mb-6 
  flex 
  items-center 
  justify-between
`;

export interface FooterProps {
  brand_text: string;
  copyright_text: string;
  footer_icons: ReactNode[];
}

const Footer: React.FC<FooterProps> = ({
  brand_text,
  copyright_text,
  footer_icons,
}) => {
  return (
    <FooterWrapper>
      <FooterBrand text={brand_text} />
      <FooterCenter children={footer_icons} />
      <FooterCopyright text={copyright_text} />
    </FooterWrapper>
  );
};

export default Footer;
