import * as React from 'react';
import classNames from 'classnames';

class ListProps {
  id: number;
  isOrdered?: boolean;
  itemStyle?: string;
  listItems: string[];
}

/**
 * @name            a-list
 * @type            atom
 * @author          Experience One AG
 * @copyright       Robert Bosch GmbH
 *
 * @param           {string} content   Array of content which should be rendered
 * @description
 * representation of lists
 */

const List: React.FunctionComponent<ListProps> = ({
  id,
  isOrdered = false,
  itemStyle = 'dot',
  listItems = [],
}: ListProps) => {
  // build custom tag for dynamic usage of ordered and unordered lists
  const CustomTag = isOrdered === true ? 'ol' : 'ul';

  // construction of class names
  const listClass = classNames('a-list', {
    [`a-list--${itemStyle}`]: itemStyle,
  });

  return (
    <CustomTag className={listClass}>
      {listItems.map(item => (
        <li key={id}>{item}</li>
      ))}
    </CustomTag>
  );
};

export { List, ListProps };
