import axios from "axios";
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
				case WorkerState.Done:
				case WorkerState.Errored:
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
		case WorkerEvent.Error: {
			return WorkerState.Errored;
		}
	}
};

const WorkerStateButton: React.FC<Props> = ({ worker }) => {
	const [state, dispatch] = useReducer(reducer, WorkerState.Waiting);

	const startWorker = async () => {
		dispatch(WorkerEvent.Start);
		await axios
			.get(
				`${import.meta.env.VITE_API_DOMAIN}/workers/${worker.id}/start`
			)
			.then(() => dispatch(WorkerEvent.Finish))
			.catch(() => dispatch(WorkerEvent.Error));
	};

	const stopWorker = async () => {
		dispatch(WorkerEvent.Stop);

		await axios
			.get(`${import.meta.env.VITE_API_DOMAIN}/workers/${worker.id}/stop`)
			.then(() => dispatch(WorkerEvent.Finish))
			.catch(() => dispatch(WorkerEvent.Error));
	};

	switch (state) {
		case WorkerState.Waiting:
			return <button onClick={startWorker}>Start Worker</button>;
		case WorkerState.Running:
			return <button onClick={stopWorker}>Working Running ...</button>;
		case WorkerState.Done:
			return <button onClick={startWorker}>Worker finished</button>;
		case WorkerState.Errored:
			return <button onClick={startWorker}>Error starting!</button>;
	}
};

export default WorkerStateButton;
