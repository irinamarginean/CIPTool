import * as React from 'react';
import classNames from 'classnames';

class TypographyProps {
  type?: string;
  size?: string;
}
/**
 * @name    Typography
 * @type    basic
 * @author Experience One AG
 * @copyright Robert Bosch GmbH
 *
 * @description
 * example page to show of basic typo usage
 */
const Typography: React.FunctionComponent<TypographyProps> = ({
  type, size
}: TypographyProps) => {

  const typographyClass = classNames({
    [type]: type,
    [`-size-${size}`]: size,
  });

  return (
    <div className={typographyClass}>This is a sample text</div>
  );
};

export { Typography, TypographyProps };
