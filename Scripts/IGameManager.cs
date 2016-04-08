// базовый интерфейс, который будет реализовывать диспетчеры данных
using System;

	public interface IGameManager
	{
		// сообщение, завершил ли модуль инициализацию
		ManagerStatus status {get;}

		// обработка процесса инициализации диспетчера
		void Startup();
	}


