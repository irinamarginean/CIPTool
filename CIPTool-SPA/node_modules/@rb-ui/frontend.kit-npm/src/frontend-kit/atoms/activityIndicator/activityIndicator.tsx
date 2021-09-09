import * as React from 'react';
import classNames from 'classnames';

class ActivityIndicatorProps {
  size?: 'medium' | 'large' | 'small';
}
/**
 * @name    a-activity-indicator
 * @type    atom
 * @author Experience One AG
 * @copyright Robert Bosch GmbH
 *
 * @param   {string}  size     given size to the activityindicator
 * @description
 * animation component while loading
 */
const ActivityIndicator: React.FunctionComponent<ActivityIndicatorProps> = ({
  size,
}: ActivityIndicatorProps) => {
  const activityIndicatorSizeClass = classNames('a-activity-indicator', {
    [`-${size}`]: size,
  });
  return (
    <div className={activityIndicatorSizeClass}>
      <div className="a-activity-indicator__top-box" />
      <div className="a-activity-indicator__bottom-box" />
    </div>
  );
};

export { ActivityIndicator, ActivityIndicatorProps };
