import * as React from 'react';
import { Button } from '../../../atoms/button/button';
import { Popover } from '../popover';

interface AttachedPopoverDemonstratorProps {
  paragraph: string;
}

const AttachedPopoverDemonstrator: React.FunctionComponent<AttachedPopoverDemonstratorProps> = ({
  paragraph,
}) => (
  <div className="frontend-kit-example_attached-popover">
    <Button label="click me" action="show" />
    <Popover
      paragraph={paragraph}
      buttonLabel="close me"
      closeButton
      detached={false}
    />
  </div>
);

export default AttachedPopoverDemonstrator;
