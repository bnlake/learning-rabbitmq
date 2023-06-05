import { useEffect, useState } from "react";
import {
	HubConnection,
	HubConnectionBuilder,
	LogLevel,
} from "@microsoft/signalr";

import Worker from "../models/Worker";
import WorkerStateButton from "./WorkerStateButton";
import { WorkerState } from "../models/WorkerState";

export interface Props {
	worker: Worker;
}

const WorkerComponent: React.FC<Props> = ({ worker }) => {
	const [connection, setConnection] = useState<HubConnection | null>(null);
	const [workerState, setWorkerState] = useState(WorkerState.Waiting);

	useEffect(() => {
		const newConnection = new HubConnectionBuilder()
			.withUrl(`${import.meta.env.VITE_API_URL}/workerhub`)
			.withAutomaticReconnect()
			.configureLogging(LogLevel.Information)
			.build();
		setConnection(() => newConnection);
	}, []);

	useEffect(() => {
		if (connection) {
			connection
				.start()
				.then(() => {
					connection.invoke("JoinGroup", worker.id);
					connection.on("ReceiveWorkerState", handleMessage);
				})
				.catch((err) => console.error(err));
		}
	}, [connection, worker.id]);

	function handleMessage(message: string) {
		console.log(message);
		switch (message) {
			case "start":
				setWorkerState(() => WorkerState.Running);
				break;
			case "finish":
				setWorkerState(() => WorkerState.Done);
				break;
			case "error":
				setWorkerState(() => WorkerState.Errored);
				break;
			default:
				setWorkerState(() => WorkerState.Waiting);
				break;
		}
	}

	return (
		<div className="card">
			<div>
				<h2>Worker</h2>
				<p>Id: {worker.id}</p>
				<p>Name: {worker.name}</p>
			</div>
			<div>
				<WorkerStateButton
					worker={worker}
					state={workerState}
					connection={connection}
				/>
			</div>
		</div>
	);
};

export default WorkerComponent;
