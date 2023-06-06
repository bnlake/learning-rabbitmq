import Worker from "../models/Worker";
import { WorkerState } from "../models/WorkerState";
import { HubConnection } from "@microsoft/signalr";

interface Props {
	worker: Worker;
	state: WorkerState;
	connection: HubConnection | null;
}

const WorkerStateButton: React.FC<Props> = ({ worker, state, connection }) => {
	const startWorker = async () => {
		connection?.invoke("StartWorker", worker.id);
	};

	const stopWorker = async () => {
		connection?.invoke("StopWorker", worker.id);
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
