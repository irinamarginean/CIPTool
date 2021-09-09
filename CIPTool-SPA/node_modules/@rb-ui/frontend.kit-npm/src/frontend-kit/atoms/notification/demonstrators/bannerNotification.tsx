import * as React from 'react';
import { Notification } from '../notification';
import { Button } from '../../button/button';

interface BannerNotificationProps {
  notificationId: string;
  iconName?: string;
  type?: 'neutral' | 'success' | 'warning' | 'error';
}

const BannerNotificationDemonstrator: React.FunctionComponent<BannerNotificationProps> = ({
  notificationId,
  type = 'neutral',
  iconName = null,
}) => (
  <div
    className="frontend-kit-example_banner-notification"
    data-frok-notification={notificationId}
  >
    <Button action="show" label="Click here" />
    <Notification
      notificationId={notificationId}
      variant="banner"
      type={type}
      iconName={iconName}
    />
  </div>
);

export default BannerNotificationDemonstrator;
