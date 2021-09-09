import * as React from 'react';
import classNames from 'classnames';
import { Text } from '../text/text';

class DividerProps {
  paragraph?: {
    paragraph: string[];
  }[];
}

/**
 * @name        a-divider
 * @type        divider
 * @author      Experience One AG
 * @copyright   Robert Bosch GmbH
 *
 * @param   {paragraph}     the text to show in the Horizontal Divider within Text/Paragraphs example
 *
 * @description
 * representation of dividers
 */
const Divider: React.FunctionComponent<DividerProps> = ({
  paragraph,
}: DividerProps) => {
  const dividerClass = classNames('a-divider', {
    [`-within-text`]: paragraph,
  });

  return (
    <>
      {paragraph ? (
        <div className="container">
          <Text content={paragraph} />
          <hr className={dividerClass} />
          <Text content={paragraph} />
        </div>
      ) : (
        <hr className={dividerClass} />
      )}
    </>
  );
};

export { Divider, DividerProps };
