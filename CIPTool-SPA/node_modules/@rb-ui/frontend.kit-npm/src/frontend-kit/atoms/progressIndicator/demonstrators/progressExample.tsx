import * as React from 'react';
import { ProgressIndicator } from '../progressIndicator';

const ProgressExampleDemonstrator: React.FunctionComponent = () => (
  <div className="frontend-kit-example_progress-indicator">
    <ProgressIndicator
      type="determinate"
      progressId="random-progress"
      progress={0}
    />
  </div>
);

export default ProgressExampleDemonstrator;
