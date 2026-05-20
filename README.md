# Just Sandbox

[English Version](#english-version) | [Русская версия](#русская-версия)

---

<a name="english-version"></a>
# 🇬🇧 English Version

**Just Sandbox** is a dynamic 3D sandbox where reflexes and chaos management are the keys to success. Eliminate enemies, earn money, and spend it on weapons, upgrades, and gear in the shop. Play your way: experiment with your arsenal, test ideas in combat, and survive in a world that shows no mercy.

## 🚀 Core Technologies & Approaches
- **Clean Architecture + MVP** — All features are isolated, testable, and extensible.
- **Feature-first Structure** — Every feature is autonomous (Application, Domain, Infrastructure, Installer, Presentation, Data).
- **SOLID Principles** — Architecture built for change and easy scaling.

## 🧠 Architecture & Patterns
- **Dependency Injection (Zenject)** — Managing dependencies and decoupling components.
- **MVP (Model-View-Presenter)** — Strict separation of logic and UI.
- **Factory / Abstract Factory** — Creating objects without tight coupling.
- **State Machine** — Managing game and feature states.

## ⚙️ Tech Stack
- **Unity Addressables** — Dynamic resource loading and management.
- **Async/Await (UniTask / Task)** — Non-blocking logic, eliminating Coroutines.
- **GamePush SDK** — Cloud saves, monetization, and rewarded ads.
- **ScriptableObjects** — Data-driven configuration approach.

## 🧪 Testing & Quality Assurance
- **NUnit** — Unit testing for core business logic.
- **Moq** — Mocking dependencies for isolated tests.
- **Unity Test Framework** — Integration and playmode testing.

## 📊 Performance & Production
- **Unity Profiler** — Deep performance analysis.
- **Memory Management** — Strict control over allocations and GC.
- **Production-Ready Build** — Optimized for stability and speed.
- **Metrics & Logging** — Real-time application state tracking.

---

## 🏗 Project Structure (Feature-first)
`📦 Assets / Features / FeatureName`
- `Application` — Use-cases & core logic
- `Domain` — The "Source of Truth"
- `Infrastructure` — Framework-dependent implementations
- `Installer` — Zenject DI bindings
- `Presentation` — UI layer
- `Data` — Configs & DTOs

## ✨ Highlights
- **Shop & Toolbox:** Dynamic entity spawning controlled by async Addressables.
- **Combat System:** Layered architecture (Input → Business Rules → Domain Validation → Execution).
- **Entity System:** Unified `IEntity` & `ITarget` interfaces for NPCs and players.
- **AI System:** FSM-based, modular behavior controllers for high flexibility.

## 🎮 Play & Source
1. **Full Game:** [yandex.ru/games/app/460639](https://yandex.ru/games/app/460639)
2. **MVP Source Code:** [s3.eponesh.com/games/22567/](https://s3.eponesh.com/games/22567/)

---

<a name="русская-версия"></a>
# 🇷🇺 Русская версия

**Just Sandbox** — динамичная 3D-песочница, где всё решает твоя реакция и умение управлять хаосом. Убивай врагов, зарабатывай деньги и трать их в магазине. Играй так, как хочешь: экспериментируй с арсеналом и выживай в мире, который не прощает ошибок.

## 🚀 Основные технологии и подходы
- **Clean Architecture + MVP** — все фичи изолированы и легко тестируются.
- **Feature-first структура** — каждая фича полностью автономна.
- **SOLID принципы** — архитектура, устойчивая к изменениям.

## 🧠 Архитектура и паттерны
- **Dependency Injection (Zenject)** — управление зависимостями и слабая связанность.
- **MVP (Model-View-Presenter)** — разделение логики и UI.
- **Factory / Abstract Factory** — создание объектов без жесткой привязки.
- **State Machine** — управление состояниями игры.

## ⚙️ Технологии
- **Unity Addressables** — динамическая загрузка ресурсов.
- **Async/Await (UniTask / Task)** — асинхронная логика без корутин.
- **GamePush SDK** — облачные сохранения, монетизация.
- **ScriptableObjects** — data-driven подход.

## 🧪 Тестирование
- **NUnit** — юнит-тестирование бизнес-логики.
- **Moq** — мокирование зависимостей.
- **Unity Test Framework** — интеграционные и playmode тесты.

## 📊 Оптимизация
- **Unity Profiler** — анализ производительности.
- **Memory Management** — контроль аллокаций и GC.
- **Production-ready** — стабильная сборка с учетом оптимизаций.

---

## 🏗 Структура проекта (Feature-first)
`📦 Assets / Features / FeatureName`
- `Application` — юз-кейсы, чистая логика
- `Domain` — корень фичи
- `Infrastructure` — реализации, зависящие от фреймворка
- `Installer` — DI Installer
- `Presentation` — UI слой
- `Data` — конфиги, DTO

## ✨ Ключевые фичи
- **Магазин и Toolbox:** асинхронный спавн через Addressables.
- **Система стрельбы:** многослойная архитектура (Input → Rules → Domain → Execution).
- **Игрок и NPC:** единые интерфейсы `IEntity` и `ITarget` для расширяемости.
- **AI:** FSM-система с набором независимых контроллеров.

## 🎮 Ссылки
1. **Игра:** [yandex.ru/games/app/460639](https://yandex.ru/games/app/460639)
2. **MVP (исходный код):** [s3.eponesh.com/games/22567/](https://s3.eponesh.com/games/22567/)
