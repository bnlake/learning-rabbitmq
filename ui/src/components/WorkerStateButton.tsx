import { useReducer } from "react";

import Worker from "../models/Worker";
import { WorkerState } from "../models/WorkerState";
import { WorkerEvent } from "../models/WorkerEvent";

interface Props {
	worker: Worker;
}

const reducer = (state: WorkerState, action: WorkerEvent) => {
	switch (action) {
		case WorkerEvent.Start: {
			switch (state) {
				case WorkerState.Waiting:
					return WorkerState.Running;
				case WorkerState.Done:
					return WorkerState.Running;
				default:
					return state;
			}
		}
		case WorkerEvent.Stop: {
			switch (state) {
				case WorkerState.Running:
					return WorkerState.Waiting;
				default:
					return state;
			}
		}
		case WorkerEvent.Finish: {
			switch (state) {
				case WorkerState.Running:
					return WorkerState.Done;
				default:
					return state;
			}
		}
		default:
			return state;
	}
};

const WorkerStateButton: React.FC<Props> = ({ worker }) => {
	const [state, dispatch] = useReducer(reducer, WorkerState.Waiting);

	const startWorker = () => {
		dispatch(WorkerEvent.Start);
		// make http request
		setTimeout(() => {
			dispatch(WorkerEvent.Finish);
		}, 2000);
	};

	const stopWorker = () => {
		dispatch(WorkerEvent.Stop);
		// make http request
	};

	switch (state) {
		case WorkerState.Waiting:
			return <button onClick={startWorker}>Start Worker</button>;
		case WorkerState.Running:
			return <button onClick={stopWorker}>Working Running ...</button>;
		case WorkerState.Done:
			return <button onClick={startWorker}>Worker finished</button>;
	}
};

export default WorkerStateButton;
